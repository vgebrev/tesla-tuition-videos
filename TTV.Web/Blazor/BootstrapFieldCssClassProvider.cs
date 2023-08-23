using Microsoft.AspNetCore.Components.Forms;

namespace TTV.Web.Blazor;

public class BootstrapFieldCssClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
        return isValid ? "is-valid" : "is-invalid";
    }
}
