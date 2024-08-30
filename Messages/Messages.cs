namespace Messages
{

    public interface INewPatientRegistered
    {
        Guid patientId { get; }
        string patientFirstName { get; }
        string patientSecondName { get; }
        string patientPesel {  get; }
        DateTime patientDateOfBirth { get; }
        string patientCity { get; }
        string patientPostalCode { get; }
        string patientStreet { get; }
        string patientPhoneNumber { get; }
        int patientHouseNumber { get; }
        int patientApartmentNumber { get; }
    }

    public interface IPatientVisitRegisteredMessage
    {
        Guid patientId { get; }
        string patientPesel { get; }
        Guid visitId { get; }
        WardType wardType { get; }
    }

    public interface IPatientVisitUnregisteredMessage
    {
        Guid patientId { get; }
        string patientPesel { get; }
        Guid visitId { get; }
    }

    public interface IPatientAllowedToLeave
    {
        Guid patientId { get; }
        string patientPesel { get; }
        Guid doctorId { get; }
        string doctorPwzNumber { get; }
    }
}
