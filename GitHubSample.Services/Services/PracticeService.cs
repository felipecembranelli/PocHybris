using GitHubSample.Data.Infrastructure;
using GitHubSample.Data.Repository;
using GitHubSample.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services
{
    public class PracticeService : IPracticeService
    {
        private readonly IPracticeRepository practiceRepository;
        private readonly IUnitOfWork unitOfWork;

        public PracticeService(IPracticeRepository practiceRepository, IUnitOfWork unitOfWork)
        {
            this.practiceRepository = practiceRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Model.Practice> GetAll()
        {
            return this.practiceRepository.GetAll();
        }

        public Model.Practice GetById(int id)
        {
            return this.practiceRepository.GetById(id);
        }

        public void Add(Model.Practice practice)
        {
            this.practiceRepository.Add(practice);

            this.unitOfWork.Commit();
        }

        public void Update(Model.Practice practice)
        {
            this.practiceRepository.Update(practice);

            this.unitOfWork.Commit();
        }

        public void Delete(Model.Practice practice)
        {
            this.practiceRepository.Delete(practice);

            this.unitOfWork.Commit();
        }


        public IEnumerable<Model.Practice> GetByPerspectiveId(int perspectiveId)
        {
            return this.practiceRepository.GetByPerspectiveId(perspectiveId);
        }
    }
}
