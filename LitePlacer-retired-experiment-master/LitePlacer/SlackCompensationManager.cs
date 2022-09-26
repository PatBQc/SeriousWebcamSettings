using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LitePlacer {
    public class SlackCompensationManager {

        private bool lastMovementWasInCompensatedDirection = false;
        private double distance = 0;

        private double _minimumRequired = 10; //for slackcompensation
        public double RequiredDistance{set { _minimumRequired = value; } get { return _minimumRequired; } }

        public bool NeedsSlackCompensation(double oldA, double newA) {
           // Console.Write("SlackComp {0} -> {1} :", oldA, newA);
            if (oldA == newA) {
           //     Console.WriteLine("false");
                return false;
            }
            if (oldA < newA) {
                //CW
                if (!lastMovementWasInCompensatedDirection) {
                    lastMovementWasInCompensatedDirection = true;
                    distance = newA - oldA;
                } else {
                    distance += newA - oldA;
                }
            } else if (oldA > newA) {
                //CCW
                lastMovementWasInCompensatedDirection = false;
                distance = 0;
            }
            if (lastMovementWasInCompensatedDirection && distance >= _minimumRequired) {
            //    Console.WriteLine("false");
                return false;
            }
           // Console.WriteLine("true");
            return true;
        }

        public  void AppliedSlackCompensation() {
            lastMovementWasInCompensatedDirection = true;
            distance = _minimumRequired;
        }


    }
}
