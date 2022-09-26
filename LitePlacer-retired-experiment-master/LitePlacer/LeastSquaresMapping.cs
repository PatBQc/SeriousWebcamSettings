using System;
using System.Collections.Generic;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace LitePlacer {
    // back computes a rigid transformation 
    // based on http://nghiaho.com/?page_id=671
    // and https://en.wikipedia.org/wiki/Kabsch_algorithm
    public class LeastSquaresMapping {
        readonly List<PartLocation> source;
        readonly List<PartLocation> dest;
        private Matrix<double> Rotation, Offset, Scale;

        private double ScaleX, ScaleY, OffsetX, OffsetY, Rotation_;

        public LeastSquaresMapping(List<PartLocation> from, List<PartLocation> to) { 
            source = from;
            dest = to;
            //Recompute();
            //Recompute2();
            Recomputer3();
        }

        public override string ToString() {
/*            return String.Format("Offset = {0}\nAngle = {1}",
                new PartLocation(Offset) - Global.Instance.Locations.GetLocation("PCB Zero"),
                Angle);*/
            return String.Format("Offset = {0}\nAngle = {1}",
                OffsetX - Global.Instance.Locations.GetLocation("PCB Zero").X,
                Rotation_);
        }

        public double Angle { 
            get { 
                //return  Math.Asin(Rotation[0, 1]) * -180d / Math.PI;
                return Math.Asin(Rotation_) * -180d / Math.PI;
            } 
        }
        private Matrix<double> _sc, _dc;
        private Matrix<double> SourceCentroid { get { if (_sc == null) _sc = PartLocation.Average(source).ToMatrix(); return _sc; } }
        private Matrix<double> DestCentroid { get { if (_dc == null) _dc = PartLocation.Average(dest).ToMatrix(); return _dc; } }

        public Matrix<double> P{
            get {
                Matrix<double> p = new Matrix<double>(source.Count, 2);
                for (int i=0; i<source.Count; i++) { p[i, 0] = source[i].X;  p[i, 1] = source[i].Y;}
                return p;
            }
        } 

        public Matrix<double> Q{
            get {
                Matrix<double> p = new Matrix<double>(dest.Count, 2);
                for (int i=0; i<dest.Count; i++) { p[i, 0] = dest[i].X; p[i, 1] = dest[i].Y;  }
                return p;
            }
        }

        public string FormatMatrix(Matrix<double> m) {
            string s = "";
            for (int i = 0; i < m.Rows; i++) {
                for (int j = 0; j < m.Cols; j++) {
                    s += String.Format("{0:F1} \t", m[i, j]);
                }
                s += "\r\n";
            }
            return s;
        }

        public void Recomputer3()
        {
            // calculate centroids
            double sourceCentroidX = 0, sourceCentroidY = 0, destCentroidX = 0, destCentroidY = 0;

            int fiducialCount = 0;

            List<PartLocation> sourceFiducials = new List<PartLocation>();
            List<PartLocation> destFiducials = new List<PartLocation>();

            PartLocation tempDest;

            foreach (var fiducial in source.Where(part => part.physicalComponent.IsFiducial).ToArray())
            {
                sourceCentroidX += fiducial.physicalComponent.X_nominal;
                sourceCentroidY += fiducial.physicalComponent.Y_nominal;
                sourceFiducials.Add(fiducial);

                tempDest = dest[source.IndexOf(fiducial)];

                if (tempDest.physicalComponent != null) {
                    destCentroidX += tempDest.physicalComponent.X_machine;
                    destCentroidY += tempDest.physicalComponent.Y_machine;
                } else {
                    destCentroidX += tempDest.X;
                    destCentroidY += tempDest.Y;
                }
                destFiducials.Add(tempDest);

                fiducialCount++;
            }
            sourceCentroidX /= fiducialCount;
            sourceCentroidY /= fiducialCount;

            destCentroidX /= fiducialCount;
            destCentroidY /= fiducialCount;

            OffsetX = -sourceCentroidX + destCentroidX;
            OffsetY = -sourceCentroidY + destCentroidY;

            List <double> destRotation = new List<double>();
            List <double> sourceRotation = new List<double>();
            List <double> destOffset = new List<double>();
            List <double> sourceOffset = new List<double>();
            List <double> scale = new List<double>();
            List<double> rotationOffset = new List<double>();

            ScaleX = 0; ScaleY = 0; Rotation_ = 0;

            // Calculate offsets from centroids, rotation from source to destination and scale difference
            PartLocation sourceFiducial = null, destFiducial = null;
            for (int i=0; i<fiducialCount; i++) {
                sourceFiducial = sourceFiducials.ElementAt(i);
                destFiducial = destFiducials.ElementAt(i);

                sourceOffset.Add(Math.Sqrt(Math.Pow(sourceFiducial.X - 
                    sourceCentroidX, 2) + Math.Pow(sourceFiducial.Y -
                    sourceCentroidY, 2)));
                destOffset.Add(Math.Sqrt(Math.Pow(destFiducial.X -
                    destCentroidX, 2) + Math.Pow(destFiducial.Y -
                    destCentroidY, 2)));

                sourceRotation.Add(Math.Asin((sourceFiducial.Y - sourceCentroidY) /
                    sourceOffset[i]));
                if ((sourceFiducial.X - sourceCentroidX) < 0) { 
                    sourceRotation[i] = Math.PI - sourceRotation[i]; }
                if (sourceRotation[i] < 0) { sourceRotation[i] += Math.PI * 2; }
                
                destRotation.Add(Math.Asin((destFiducial.Y - destCentroidY) /
                    destOffset[i]));
                if ((destFiducial.X - destCentroidX) < 0) {
                    destRotation[i] = Math.PI - destRotation[i]; }
                if (destRotation[i] < 0) { destRotation[i] += Math.PI * 2; }

                rotationOffset.Add(destRotation[i] - sourceRotation[i]);

                scale.Add(destOffset[i] / sourceOffset[i]);

                Rotation_ += rotationOffset[i];
                ScaleX += (destFiducial.X - destCentroidX) / (sourceFiducial.X - sourceCentroidX);
                ScaleY += (destFiducial.Y - destCentroidY) / (sourceFiducial.Y - sourceCentroidY);
            }

            Rotation_ /= fiducialCount;
            ScaleX /= fiducialCount;
            ScaleY /= fiducialCount;
        }

        public PartLocation Map2(PartLocation from)
        {
            if (ScaleX == 0 || ScaleY == 0) throw new Exception("LeastSquareMapping not intialized");

            var p = new PartLocation(from);

            p.X = (from.X + OffsetX) * ScaleX;
            // Why there's a -1 necessary here is beyond me but it works so
            p.Y = (from.Y + OffsetY) * ScaleY - 1;
            p.Rotate(Rotation_);

            return p;
        }

        // this is directly from the wikipedia page on Kabsh Algorithm
        public void Recompute2() {
            var p = P;
            var q = Q;

            //1. subtract centroids
            for (int i = 0; i < p.Rows; i++) {
                p[i, 0] -= SourceCentroid[0, 0];
                p[i, 1] -= SourceCentroid[1, 0];
                q[i, 0] -= DestCentroid[0, 0];
                q[i, 1] -= DestCentroid[1, 0];
            }

            //2. compute covariance matrix
            var a = p.Transpose()*q;

            //3. compute rotation matrix
            /* perform svd  where A =  V S WT */
            Matrix<double> V = new Matrix<double>(2, 2);
            Matrix<double> S = new Matrix<double>(2, 2);
            Matrix<double> W = new Matrix<double>(2, 2);
            CvInvoke.cvSVD(a.Ptr, S.Ptr, V.Ptr, W.Ptr, SVD_TYPE.CV_SVD_DEFAULT);

            // Deal with reflection matrix
            Matrix<double> m = new Matrix<double>(2, 2);
            m.SetIdentity(new MCvScalar(1));
            m[1,1] = ((W*V.Transpose()).Det<0) ? -1 : 1;

            // Comput the rotation matrix
            Rotation = W*m*V.Transpose();
            //Offset = DestCentroid - (Rotation * SourceCentroid);
            Offset = DestCentroid - SourceCentroid;

            Console.WriteLine("Rotaiton Matrix - Angle ="+Angle);
            Console.WriteLine(FormatMatrix(Rotation));
        }


        // this is from the blog entry
        public void Recompute() {
            if (source == null || dest == null || source.Count != dest.Count)
                throw new Exception("Input data null or not equal in length");

            // compute covariance matrix
            Matrix<double> H = new Matrix<double>(2, 2);
            
            H.SetZero();
            for (int i = 0; i < source.Count; i++) {
                var a = source[i].ToMatrix() - SourceCentroid;
                var b = dest[i].ToMatrix() - DestCentroid;
                H += a * b.Transpose();
            }
            
            /* perform svd  where A =  U W VT
             *  A  IntPtr  Source MxN matrix
             *  W  IntPtr  Resulting singular value matrix (MxN or NxN) or vector (Nx1).
             *  U  IntPtr  Optional left orthogonal matrix (MxM or MxN). If CV_SVD_U_T is specified, the number of rows and columns in the sentence above should be swapped
             *  V  IntPtr  Optional right orthogonal matrix (NxN)
             */
        
            Matrix<double> U = new Matrix<double>(2, 2); 
            Matrix<double> W = new Matrix<double>(2, 2);            
            Matrix<double> V = new Matrix<double>(2, 2);
            CvInvoke.cvSVD(H.Ptr, W.Ptr, U.Ptr, V.Ptr, SVD_TYPE.CV_SVD_DEFAULT);

            // compute rotational matrix R=V*UT
            Rotation = V * U.Transpose();            

            // find translation
            //Offset = DestCentroid - ( Rotation * SourceCentroid);
            Offset = DestCentroid - SourceCentroid;

            if (Angle > 5) {
                Global.Instance.mainForm.ShowSimpleMessageBox("Excessive Angle Detected - Problem detecting rotation\nOffset = " + new PartLocation(Offset-DestCentroid) + "\nAngle=" + Angle);
            }
        }

        /// <summary>
        /// Map a source point to a destination point based on the calibrated inputs
        /// </summary>
        public PartLocation Map(PartLocation from) {
            if (Rotation == null || Offset == null) throw new Exception("LeastSquareMapping not intialized");

            var x = from.ToMatrix();
            var y = (Rotation * (x - SourceCentroid)) + Offset + SourceCentroid; //shift point to center, apply rotation, then shift to the destination

            var p = new PartLocation(y) {A = from.A + Angle};
            return p;
        }

        /// <summary>
        /// The RMS error of all the source to dest points
        /// </summary>
        /// <returns></returns>
        public double RMSError() {
            double rms_error = 0;
            for (int i = 0; i < source.Count; i++) {
                var b = Map2(source[i]);
                rms_error += Math.Pow(b.DistanceTo(dest[i]), 2);
            }
            rms_error = Math.Sqrt(rms_error);
            return rms_error;
        }

        

        /// <summary>
        /// The furthest distance a fiducial moved
        /// </summary>
        /// <returns></returns>
        public double MaxFiducialMovement() {
//            Global.Instance.DisplayText(String.Format("Offset = {0}  Angle = {1}", new PartLocation(Offset) - Global.Instance.Locations.GetLocation("PCB Zero") , Angle), System.Drawing.Color.Purple);
            List<double> distances = new List<double>();
            for (int i = 0; i < source.Count; i++) {
                var s = new PartLocation(source[i]);
                var d = new PartLocation(dest[i]);
                var m = Map2(source[i]);
               // Global.Instance.DisplayText(String.Format("Source {0}  Dest {1}  Mapped {2}", s, d, m), System.Drawing.Color.Purple);
                distances.Add(Map2(source[i]).DistanceTo(dest[i]));
            }
            return distances.Max();
        }
                

    }
}
