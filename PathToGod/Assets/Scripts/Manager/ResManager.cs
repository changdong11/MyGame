using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityObject = UnityEngine.Object;
namespace ProjectPratice
{
    class ResManager:MonoManager
    {

        public UnityObject LoadLocalRes(string resPathWithSuffix,bool isResource = false)
        {
            if (isResource )
            {
                return UnityEngine.Resources.Load<UnityObject>(resPathWithSuffix);
            }
            return GameFace.instance.AssetBundleManager.Load<UnityObject>(resPathWithSuffix);

        }
        #region 生命周期函数
        protected override List<string> MessageList
        {
            get
            {
                return new List<string>()
                {
                   
                };
            }
        }
        public override void OnMessage(IConstruction notification)
        {
            base.OnMessage(notification);
            if (notification == null)
            {
                return;
            }
            string name = notification.Name;
            if (name == null)
            {
                return;
            }
            switch (name)
            {
                
                default:
                    break;

            }
        }
        protected override void Init()
        {
            
        }
        #endregion
    }
}
