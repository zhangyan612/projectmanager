using ProjectManager.DAL.Repository;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.DAL.Services
{
    public interface IProjectBoardService
    {
        ProjectBoard GetBoard(int? id);
        List<ProjectBoard> GetBoardByProject(Guid projectId);
        List<ProjectBoard> initBoard(Guid projectId);
        Task CreateBoardItem(int id, Guid pid, string boardName, string text);
        void CreateBoard(ProjectBoard board);
        void UpdateBoard(ProjectBoard board);
        void DeleteBoard(int id);
        void SaveBoard();
    }

    public class ProjectBoardService : IProjectBoardService
    {
        private readonly IProjectBoardRepository boardRepository;
        //private readonly IProjectsRepository projectsRepository;
        private readonly IUnitOfWork unitOfWork;
        //private readonly PMContext db = new PMContext();

        public ProjectBoardService(IProjectBoardRepository boardRepository, IUnitOfWork unitOfWork)
        {
            //this.projectsRepository = projectsRepository;
            this.boardRepository = boardRepository;
            this.unitOfWork = unitOfWork;
        }

        public ProjectBoard GetBoard(int? id)
        {
            var board = boardRepository.Get(u => u.Id == id);
            return board;
        }

        public List<ProjectBoard> GetBoardByProject(Guid projectId)
        {
            var boards =
                //projectsRepository.Get(u => u.Id == projectId).Boards.ToList();
                boardRepository.GetMany(u => u.ProjectId == projectId).OrderBy(b => b.Position)
                .ToList();
            return boards;
        }

        public List<ProjectBoard> initBoard(Guid projectId)
        {
            string[] defaultBoard = new string[] { "Backlog", "To Do", "In Progress", "Completed" };
            string[] css = new string[] { "default", "info", "warning", "success" };

            List<ProjectBoard> initial = new List<ProjectBoard>();
            for (int i = 0; i < defaultBoard.Length; i++)
            {
                ProjectBoard board = new ProjectBoard()
                {
                    Name = defaultBoard[i],
                    Position = i,
                    cssClass = css[i],
                    //ProjectId = projectId
                };
                initial.Add(board);
                boardRepository.Add(board);
                //var project = projectsRepository.Get(u => u.Id == projectId);
                //project.Boards = initial;
                //projectsRepository.Update(project);
            }
            SaveBoard();
            return initial;
        }

        public Task CreateBoardItem(int id, Guid pid, string boardName, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Task newTask = new Task()
                {
                    BoardId = id
                };

                switch (boardName)
                {
                    case "In Progress":
                        //db.Tasks.Add(newTask);
                        break;
                    case "Completed":
                        //db.Tasks.Add(newTask);
                        break;
                    default:
                        //db.Tasks.Add(newTask);
                        break;
                }
            }
            return null;
        }


        public void CreateBoard(ProjectBoard board)
        {
            boardRepository.Add(board);
            SaveBoard();
        }

        public void UpdateBoard(ProjectBoard board)
        {
            boardRepository.Update(board);
            SaveBoard();
        }

        public void DeleteBoard(int id)
        {
            var p = boardRepository.Get(u => u.Id == id);
            boardRepository.Delete(p);
            SaveBoard();
        }

        public void SaveBoard()
        {
            unitOfWork.Commit();
        }

    }
}