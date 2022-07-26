using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DTO
    {
        public const string IDColumnName = "id";
        protected DalController _controller;
        public int id { get; set; } = -1;
        protected DTO(DalController controller)
        {
            _controller = controller;
        }
    }
}
