﻿using Microsoft.AspNetCore.Components;
using MyQuranWebRepository.Interfaces;

namespace MyQuranWeb.Blazor.Pages
{
    public class PageBase : ComponentBase
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; init; }

        
        protected string ErrorMessage;
        protected string PageTitle;
    }
}
