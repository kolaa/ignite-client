namespace ignite_client
{
    public class IgniteSettings
    {
        public string[] Endpoints { get; set; } = {"127.0.0.1"};

        public string CertificatePath { get; set; } = "";

        public string CertificatePassword { get; set; } = "";

        public bool SkipServerCertificateValidation { get; set; } = false;
    }
}
