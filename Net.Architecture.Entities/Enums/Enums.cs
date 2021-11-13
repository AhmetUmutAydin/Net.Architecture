using System.ComponentModel;

namespace Net.Architecture.Entities.Enums
{
    public static class Enums
    {
        public enum Gender
        {
            /// <summary>
            /// Parent
            /// </summary>
            Parent = 10,
            /// <summary>
            /// Erkek
            /// </summary>
            [Description("Erkek")]
            Male = 10001,
            /// <summary>
            /// Kadın
            /// </summary>
            [Description("Kadın")]
            Female = 10002
        }


        public enum CommunicationType
        {
            /// <summary>
            /// Parent
            /// </summary>
            Parent = 30,
            /// <summary>
            /// Telefon Numarası
            /// </summary>
            [Description("Telefon")]
            Phone = 30001,
            /// <summary>
            /// Cep Telefonu Numarası
            /// </summary>
            [Description("Cep Telefonu")]
            MobilePhone = 30002,
            /// <summary>
            /// Email Adresi
            /// </summary>
            [Description("Email")]
            Email = 30003,

        }
    }
}
