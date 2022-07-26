using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    public class BoardsMembersDTO : DTO
    {
        public const string boardIdColumn = "board_id";
        public const string memberIdColumn = "member_id";

        private int boardId;

        public int BoardId { get => boardId; set { boardId = value; _controller.Update(id, boardIdColumn, value); } }

        private int memberID;
        public int MemberID { get => memberID; set { memberID = value; _controller.Update(id, memberIdColumn, value); } }

        public BoardsMembersDTO(int boardID, int memberID) : base(new BoardsMembersDalController())
        {
            this.boardId = boardID;
            this.memberID = memberID;
        }
    }
}
