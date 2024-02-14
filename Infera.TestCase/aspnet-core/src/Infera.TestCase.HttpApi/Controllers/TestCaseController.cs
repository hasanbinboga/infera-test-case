using Infera.TestCase.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Infera.TestCase.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TestCaseController : AbpControllerBase
{
    protected TestCaseController()
    {
        LocalizationResource = typeof(TestCaseResource);
    }
}
