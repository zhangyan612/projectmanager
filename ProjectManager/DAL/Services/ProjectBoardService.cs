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
        void CreateBoard(ProjectBoard board);
        void UpdateBoard(ProjectBoard board);
        void DeleteBoard(int id);
        void SaveBoard();
    }

    public class ProjectBoardService : IProjectBoardService
    {
        private readonly IProjectBoardRepository boardRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProjectBoardService(IProjectBoardRepository boardRepository, IUnitOfWork unitOfWork)
        {
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
            var project = boardRepository.GetMany(u => u.ProjectId == projectId).OrderBy(b => b.Position).ToList();
            return project;
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
                    ProjectId = projectId
                };
                initial.Add(board);
                boardRepository.Add(board);
            }
            SaveBoard();
            return initial;
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