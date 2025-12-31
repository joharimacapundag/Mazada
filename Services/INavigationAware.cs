using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazada.Services
{
    interface INavigationAware<TArgs>
    {
        void OnNavigatedTo(INavigation navigation, TArgs parameter);
    }
}
