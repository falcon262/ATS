using Volo.Abp.Settings;

namespace ATS.Settings;

public class ATSSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ATSSettings.MySetting1));
    }
}
