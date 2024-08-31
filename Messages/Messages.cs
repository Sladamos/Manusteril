namespace Messages
{

    public interface INewPatientRegistered
    {
        Guid PatientId { get; }
        string PatientFirstName { get; }
        string PatientSecondName { get; }
        string PatientPesel {  get; }
        DateTime PatientBirthDate { get; }
        string PatientCity { get; }
        string PatientPostalCode { get; }
        string PatientStreet { get; }
        string PatientPhoneNumber { get; }
        int PatientHouseNumber { get; }
        int PatientApartmentNumber { get; }
    }

    public interface IPatientDataChanged
    {
        Guid PatientId { get; }
        string PatientFirstName { get; }
        string PatientSecondName { get; }
        string PatientPesel {  get; }
        DateTime PatientBirthDate { get; }
        string PatientCity { get; }
        string PatientPostalCode { get; }
        string PatientStreet { get; }
        string PatientPhoneNumber { get; }
        int PatientHouseNumber { get; }
        int PatientApartmentNumber { get; }
    }

    public interface IPatientVisitRegisteredMessage
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
        WardType WardType { get; }
    }

    public interface IPatientVisitUnregisteredMessage
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid VisitId { get; }
    }

    public interface IPatientAllowedToLeave
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid DoctorId { get; }
        string DoctorPwzNumber { get; }
        bool LeavedAtOwnRisk { get; }
    }

    public interface IPatientWardChanged
    {
        Guid PatientId { get; }
        string PatientPesel { get; }
        Guid DoctorId { get; }
        string DoctorPwzNumber { get; }
        WardType Destination { get; }
    }
}
