using System;
using System.Collections.Generic;
using System.Text;
using Infera.TestCase.Localization;
using Volo.Abp.Application.Services;

namespace Infera.TestCase;

/* Inherit your application services from this class.
 */
public abstract class TestCaseAppService : ApplicationService
{
    protected TestCaseAppService()
    {
        LocalizationResource = typeof(TestCaseResource);
    }
}
