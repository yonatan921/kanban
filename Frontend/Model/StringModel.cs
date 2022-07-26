using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class StringModel : NotifiableModelObject
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                this.name = value;
                // RaisePropertyChanged("id");
            }
        }

        public StringModel(BackendController bc, string name) : base(bc)
        {
            this.name = name;
        }
    }
}
