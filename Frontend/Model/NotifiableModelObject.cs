using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.ViewModel;

namespace Frontend.Model
{
    public abstract class NotifiableModelObject : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        protected NotifiableModelObject(BackendController controller)
        {
            this.Controller = controller;
        }
    }
}
