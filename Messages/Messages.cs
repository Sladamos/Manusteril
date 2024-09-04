namespace Messages
{

    public interface INewPatientRegistered
    {
        Guid PatientId { get; }
        string PatientFirstName { get; }
        string PatientLastName { get; }
        string PatientPesel {  get; }
        string PatientCity { get; }
        string PatientAddress { get; }
        string PatientPhoneNumber { get; }
    }

    public interface IPatientDataChanged
    {
        Guid PatientId { get; }
        string PatientFirstName { get; }
        string PatientLastName { get; }
        string PatientPesel { get; }
        string PatientCity { get; }
        string PatientAddress { get; }
        string PatientPhoneNumber { get; }
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

    public interface IIsPatientInsured
    {
        string PatientPesel { get; }
    }

    public interface IIsPatientInsuredResponse
    {
        string PatientPesel { get; }
        bool IsInsured { get; }
    }
}
