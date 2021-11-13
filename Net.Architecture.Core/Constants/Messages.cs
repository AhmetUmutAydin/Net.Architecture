namespace Net.Architecture.Core.Constants
{
    public static class Messages
    {
        public static string GlobalError = "Hata işlem tamamlanamadı!";
        public static string WrongValidationType = "Yanlış doğrulama tipi.";
        public static string WrongDeciderType = "Yanlış decider tipi.";
        public static string NotFoundUpdate = "Güncellenmesi istenen veri bulanamadı.";
        public static string EmailExists = "Email daha önceden kullanılmış.";
        public static string EmailNotFound = "Kişisinin email adresi bulunamadı.";
        public static string UsernameExists = "Kullanıcı adı daha önceden kullanılmış.";
        public static string UserExists = "Kullanıcı daha önceden üye olmuş.";
        public static string UserNotFound = "Kullanıcı bulunamadı!";
        public static string DemoUserCannotChange = "Demo kullancısı olduğunuz için bu değişikliği yapamazsınız!";

        public static string WrongPassword = "Şifre hatalı!";
        public static string WrongRefreshToken = "Oturum süresi sonlandı.";
        public static string ClientLoginFailed = "ClientId or ClientSecret not found!";
        public static string WrongInvitation = "Davetiye yok veya hatalı!";
        public static string InvitationExpired = "Davetiyenin süresi dolmuş!";
        public static string EmployeeNotFound = "Çalışan bulunamadı!";
        public static string InvitationRegisterLink = "Kayıt olmak için tıklayınız.";
        public static string InvitationLoginLink = "Giriş yapmak için tıklayınız.";
        public static string InvitationTrainingLink = "Dersi onaylamak için tıklayınız.";
        public static string FileNotFound = "Dosya bulunamadı!";
        public static string BranchNotFound = "Şube bulunamadı!";
        public static string InstitutionNotFound = "Kurum bulunamadı!";
        public static string MemberNotFound = "Üye bulunamadı!";
        public static string CentralBranchDeleteError = "Merkez şube silinemez! Lütfen merkez şubenizi değiştiriniz!";
        public static string CentralBranchCannotChange = "Başka bir şubenizi merkez şube olarak atayanız!";
        public static string AddressNotFound = "Adres bulunamadı!";
        public static string PersonalInformationNotFound = "Kişisel bilgiler bulunamadı!";
        public static string FileExtensionError = "Dosya uzantısı sisteme yüklenemez!";
        public static string FileSizeError = "Dosya boyutu çok büyük. En fazla 1 MB yüklenebilir!";
        public static string FileNameSizeError = "Dosya adı 100 karakterden büyük olamaz.";
        public static string AlreadyExists = "Kayıt zaten mevcut  yeni ekleme yapılamaz!";
        public static string TrainingNotFound = "Ders bulunamadı!";
        public static string TrainingTypeNotFound = "Ders tipi bulunamadı!";
        public static string TrainingPaymentTrainer = "Eğitmen ders ücreti";
        public static string TrainingPaymentMember = "Üye ders ücreti";
        public static string AlreadyHaveLesson = "Bu ders saatinde dersi var.";
        public static string DoesntHavePaymentAmount = " adlı kişinin bu ders tipi için ödeme bilgisi girilmemiş.";
        public static string BalanceNotFound = "Bu ödeme kaydı silinemez !!";
        public static string BalanceNotEnough = "adlı kişinin bakiyesi yetersiz";
        public static string RoleNotFound = "Yetki hatası!";

        public static string MembershipNotFound = "Kurum üyeliğiniz bulunamadı.";
        public static string MembershipExpired = "Kurum üyeliğinizin süresi dolmuştur.";
        public static string MembershipEmployeesLimit = "Kayıt edilebilecek çalışan sayısı sınırına ulaştığınız için çalışan sisteme kayıt edilemedi.";
        public static string MembershipMembersLimit = "Kayıt edilebilecek üye sayısı sınırına ulaştığınız için üye sisteme kayıt edilemedi.";
        public static string MembershipBranchesLimit = "Kayıt edilebilecek şube sayısı sınırına ulaştığınız için şube sisteme kayıt edilemedi.";
    }
}
