using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Xml.Linq;

namespace Valhalla.Componentes
{
    public abstract class PageBase : ComponentBase
    {
        [Inject] protected NavigationManager nav { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        [CascadingParameter] protected Task<AuthenticationState> AuthStat { get; set; }
        protected ClaimsPrincipal AuthenticationStateUser { get; set; }

        protected virtual List<string> Roles { get; set; }

        protected override void OnInitialized()
        {

        }

        protected override async Task OnParametersSetAsync()
        {
            AuthenticationState authenticationState = await AuthStat;
            AuthenticationStateUser = authenticationState.User;
        }

        protected virtual void Constructor()
        {

            InvokeAsync(StateHasChanged);
        }

        protected virtual void Load()
        {

            InvokeAsync(StateHasChanged);
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Constructor();
                Load();
            }
        }

        protected async Task MostraAguarde()
        {
            await InvokeAsync(StateHasChanged);
        }

        protected async Task EscondeAguarde()
        {
            await InvokeAsync(StateHasChanged);
        }

        //protected void MostraValidacao(ValidacaoRepositorioException ex)
        //{
        //    //mensagemModal.Mostrar("Confira os campo(s) abaixo", (MarkupString)string.Join("<br>", ex.GetValidationErrors().SelectMany(p => p.Value)), CustomModal.TipoCustomModal.Ok, ex, "Fechar", "text-danger");
        //}

        //protected void MostraErro(Exception ex)
        //{
        //    //mensagemModal.Mostrar("E R R O", ex.Message, CustomModal.TipoCustomModal.Ok, ex, "Fechar", "text-danger");
        //}

        protected async Task SetaFocusByID(string elementID)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("focusElementByID", elementID);
            }
            catch (Exception ex)
            {
                //new ServicoLogDeGlitch(DbFactory).RegistraLog(new Exceptions.ExceptionComInner(ex), null, TipoLogEnum.Desconhecida);
            }
        }

        protected async Task SetaFocusByName(string elementName)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("focusElementByName", elementName);
            }
            catch (Exception ex)
            {
                //new ServicoLogDeGlitch(DbFactory).RegistraLog(new Exceptions.ExceptionComInner(ex), null, TipoLogEnum.Desconhecida);
            }
        }

        protected void OnChangeToUpperCase(ref string value)
        {
            value = value.ToUpper();
        }

        protected string Versao()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            return version;
        }
    }
}



