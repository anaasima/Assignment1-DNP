#pragma checksum "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\Pages\EditAdults.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b80a845d62a64de3bb795c30fd6f7915c01d61d8"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace FINALAssignment.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using FINALAssignment;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\_Imports.razor"
using FINALAssignment.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\Pages\EditAdults.razor"
using FINALAssignment.Data.AddAdultsService;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\Pages\EditAdults.razor"
using global::Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\Pages\EditAdults.razor"
using FileData;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/EditAdults/{adultToEdit}")]
    public partial class EditAdults : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 40 "C:\Users\eatcl\RiderProjects\FINALAssignment\FINALAssignment\Pages\EditAdults.razor"
       
    private string HairColor;
    private string EyeColor;
    private int Age;
    private float Weight;
    private int Height;
    private string Sex;
    private string JobTitle;
    private Adult adult;
    private Adult anotherAdult;
    private IList<Adult> adults;
    private int index;
    [Parameter]
    public String adultToEdit { get; set; }

    private FileContext _fileContext = new FileContext();
    
    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine(adultToEdit);
        adult = AddAdultsService.GetAdults().FirstOrDefault(a => String.Join("", a.Id).Equals(adultToEdit));
        Console.WriteLine(adult);
        JobTitle = adult.JobTitle;
        Sex = adult.Sex;
        HairColor = adult.HairColor;
        EyeColor = adult.EyeColor;
        Age = adult.Age;
        Weight = adult.Age;
        Height = adult.Height;
        adults = _fileContext.Adults;
    }

    public void EditAdult()
    {
        anotherAdult = adults.First(a => a.Id == adult.Id);
        anotherAdult.JobTitle = JobTitle;
        anotherAdult.Sex = Sex;
        anotherAdult.HairColor = HairColor;
        anotherAdult.EyeColor = EyeColor;
        anotherAdult.Age = Age;
        anotherAdult.Weight = Weight;
        anotherAdult.Height = Height;
        adults.First(a => a.Id == adult.Id).Update(anotherAdult);
        _fileContext.SaveChanges();
        NavigationManager.NavigateTo("/AddAdults", true);
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAddAdultsService AddAdultsService { get; set; }
    }
}
#pragma warning restore 1591
