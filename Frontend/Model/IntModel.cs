using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class IntModel : NotifiableModelObject
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                this.id = value;
                RaisePropertyChanged("id");
            }
        }

        public IntModel(BackendController bc, int id) : base(bc)
        {
            this.Id = id;
        }
    }
}
