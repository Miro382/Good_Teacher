namespace Good_Teacher.Class.Actions
{
    public static class SetVisibilityEnum
    {
        public enum SetVisibilityValue
        {
            SetToVisible,
            SetToInvisible,
            ToggleVisibility
        }

        public static string GetEnumString(SetVisibilityValue value)
        {
            if(value == SetVisibilityValue.SetToVisible)
            {
                return Strings.ResStrings.SetVisible;

            }else if (value == SetVisibilityValue.SetToInvisible)
            {
                return Strings.ResStrings.SetInvisible;
            }
            else
            {
                return Strings.ResStrings.ToggleVisibility;
            }
        }

    }
}
