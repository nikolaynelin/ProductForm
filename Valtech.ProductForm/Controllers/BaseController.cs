using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;

namespace Valtech.ProductForm.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
        protected List<string> GetErrors()
        {
            var errors = new List<string>();
            foreach (var res in ModelState.Values)
            {
                if (res.Errors.Any())
                {
                    errors.AddRange(res.Errors.Select(x => x.ErrorMessage).Where(x => !string.IsNullOrEmpty(x)));
                }
            }
            return errors;
        }

        protected List<T> MapTo<T>(IEnumerable<object> list)
        {
            return list.Select(x => _mapper.Map<T>(x)).ToList();
        }

        protected TOutput MapTo<TOutput>(object input)
        {
            return _mapper.Map<TOutput>(input);
        }
    }
}