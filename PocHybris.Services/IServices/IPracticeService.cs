using GitHubSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubSample.Services.IServices
{
    public interface IPracticeService
	{
        IEnumerable<Practice> GetAll();
        Practice GetById(int id);
        void Add(Practice process);
        void Update(Practice process);
        void Delete(Practice process);
        IEnumerable<Practice> GetByPerspectiveId(int perspectiveId);
	} 
}
