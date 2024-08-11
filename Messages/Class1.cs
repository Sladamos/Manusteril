namespace Messages
{
    public interface IPatientRegisteredMessage
    {
        Guid patientId { get; }
        String patientFirstName { get; }
        String patientSecondName { get; }
        String patientPesel {  get; }
        DateTime patientDateOfBirth { get; }
        String patientCity { get; }
        String patientPostalCode { get; }
        String patientStreet { get; }
        String patientPhoneNumber { get; }
        int patientHouseNumber { get; }
        int patientApartmentNumber { get; }
    }

    public interface IPatientUnregisteredMessage
    {
        Guid pateintId { get; }
    }
}
