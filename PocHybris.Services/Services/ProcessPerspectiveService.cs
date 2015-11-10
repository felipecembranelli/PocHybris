using GitHubSample.Data.Infrastructure;
using GitHubSample.Data.Repository;
using GitHubSample.Model;
using GitHubSample.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services
{
    public class ProcessPerspectiveService : IProcessPerspectiveService
    {
        private readonly IProcessPerspectiveRepository processPerspectiveRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProcessPerspectiveService(IProcessPerspectiveRepository processPerspectiveRepository, IUnitOfWork unitOfWork)
        {
            this.processPerspectiveRepository = processPerspectiveRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Model.ProcessPerspective> GetAll()
        {
            return this.processPerspectiveRepository.GetAll();
        }


        public Model.ProcessPerspective GetById(int id)
        {
            return this.processPerspectiveRepository.GetById(id);
        }


        public void Add(ProcessPerspective process)
        {
            this.processPerspectiveRepository.Add(process);

            this.unitOfWork.Commit();

        }


        public void Update(ProcessPerspective process)
        {
            this.processPerspectiveRepository.Update(process);

            this.unitOfWork.Commit();
        }


        public void Delete(ProcessPerspective process)
        {
            this.processPerspectiveRepository.Delete(process);

            this.unitOfWork.Commit();
        }
    }
}
