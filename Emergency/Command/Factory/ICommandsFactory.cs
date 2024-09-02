﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency.Command.AddPatient;
using Emergency.Command.CheckInsurance;
using Emergency.Command.RegisterPatient;
using Emergency.Command.UnregisterPatient;
using Emergency.Patient;
using Messages;

namespace Emergency.Command.Factory
{
    internal interface ICommandsFactory
    {
        ExitProgramCommand ExitProgramCommand();
        ExitOptionCommand ExitOptionCommand();
        UnregisterPatientCommand UnregisterPatientCommand();
        UnregisterPatientLogicCommand UnregisterPatientLogicCommand(Func<string> peselSupplier);
        AddPatientCommand AddPatientCommand();
        AddPatientLogicCommand AddPatientLogicCommand(Func<string> peselSupplier, Func<string> firstNameSupplier, Func<string> lastNameSupplier,
            Func<string> phoneNumberSupplier, Func<string> addressSupplier, Func<string> citySupplier);
        CheckInsuranceCommand CheckInsuranceCommand();
        CheckInsuranceLogicCommand CheckInsuranceLogicCommand(Func<string> getPesel);
        SelectStringCommand SelectStringCommand(string parameter, Func<string> paremeterSupplier);
        SelectWardCommand SelectWardCommand(Func<WardType?> paremeterSupplier);
        RegisterPatientCommand RegisterPatientCommand();
        RegisterPatientLogicCommand RegisterPatientLogicCommand(Func<string> getPesel, Func<WardType?> getWard);
    }
}
