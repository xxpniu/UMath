using System;

namespace UMath
{
    public class UTransform
    {
        public UTransform()
        {
            
        }

        private UVector3 _localPos = UVector3.zero;
        private UQuaternion _localRot = UQuaternion.LookRotation(UVector3.forward);
        private UVector3 _localScale =  UVector3.one;
        private bool isdrity = true; 

        public UVector3 localPosition
        {
            set
            {
                isdrity = true;
                _localPos = value;
            }

            get
            { 
                return _localPos;
            }
        }
    }
}

