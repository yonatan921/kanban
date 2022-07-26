using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardDTO : DTO
    {
        public const string BoardsBoardNameColumnName = "board_name";
        public const string BoardsBoardOwnerIdColumnName = "board_owner";
        public const string BoardsBacklogLimitColumnName = "backlog";
        public const string BoardsInProgressLimitColumnName = "in_progress";
        public const string BoardsDoneLimitColumnName = "done";

        private string boardName;
        public string BoardName { get => boardName; set { boardName = value; _controller.Update(id, BoardsBoardNameColumnName, value); } }

        private int boardOwnerId;
        public int BoardOwnerId { get => boardOwnerId; set { boardOwnerId = value; _controller.Update(id, BoardsBoardOwnerIdColumnName, value); } }

        private int backlogLimit;
        public int BacklogLimit { get => backlogLimit; set { backlogLimit = value; _controller.Update(id, BoardsBacklogLimitColumnName, value); } }

        private int inProgressLimit;
        public int InProgressLimit { get => inProgressLimit; set { inProgressLimit = value; _controller.Update(id, BoardsInProgressLimitColumnName, value); } }

        private int doneLimit;
        public int DoneLimit { get => doneLimit; set { doneLimit = value; _controller.Update(id, BoardsDoneLimitColumnName, value); } }

        public BoardDTO(int id, string boardName, int boardOwnerId, int backlogLimit, int inProgressLimit,
            int doneLimit) : base(new BoardDalController())
        {
            this.id = id;
            this.boardName = boardName;
            this.boardOwnerId = boardOwnerId;
            this.backlogLimit = backlogLimit;
            this.inProgressLimit = inProgressLimit;
            this.doneLimit = doneLimit;
        }

    }
}
