using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phani.StrategyPattern.Models;
using Phani.StrategyPattern.Providers;
using Phani.StrategyPattern.Repository;

namespace Phani.StrategyPattern.Controllers
{
    [ApiController]
    [Route("[developers]")]
    public class DeveloperController : ControllerBase
    {
        private readonly ILogger<DeveloperController> _logger;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IEnumerable<IDeveloperProvider> _developerProvider;

        public DeveloperController(ILogger<DeveloperController> logger,
                                    IDeveloperRepository developerRepository,
                                    IEnumerable<IDeveloperProvider> developerProvider)
        {
            _logger = logger;
            _developerRepository = developerRepository;
            _developerProvider = developerProvider;
        }

        [HttpGet("{id}/developers")]
        public ActionResult<Language> GetPromotionToken(string id)
        {
            var details = _developerRepository.GetDeveloperDetails(id);
            var response = new Language();
            var result = _developerProvider.FirstOrDefault(x => x.LanguageType == details.LanguageType)?.GetLanguage();

            if(result == null)
            {
                throw new NotImplementedException();
            }

            response.Result = result;

            return response;
        }
    }
}
