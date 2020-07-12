using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNETProject
{
    public class Engine
    {
        public void run()
        {

            string[] inputData = collectAndVerifyInputData();

            string filePath = inputData[0];
            string senderMail = inputData[1];
            string password = inputData[2];
            string receiverMail = inputData[3];

            string aggregatedDataDirectory = createAggregatedFileAndReturnItsDirectory(filePath);

            sendMail(senderMail, password, receiverMail, aggregatedDataDirectory);

            Console.WriteLine("Sent successfully !");

            Environment.Exit(0);
        }

        private string[] collectAndVerifyInputData()
        {

            Console.WriteLine("Enter e-mail of sender:");
            string senderMail = Console.ReadLine();
            verifyMailOfSender(senderMail);


            Console.WriteLine("Enter password of the sender:");
            string password = Console.ReadLine();
            verifyPassword(password);

            Console.WriteLine("Enter e-mail of receiver:");
            string receiverMail = Console.ReadLine();
            verifyMailOfReceiver(receiverMail);

            Console.WriteLine("Enter filepath:");
            string filePath = Console.ReadLine();
            verifyFilePath(filePath);

            string[] collectedData = new string[4];
            collectedData[0] = filePath;
            collectedData[1] = senderMail;
            collectedData[2] = password;
            collectedData[3] = receiverMail;

            return collectedData;
        }

        private void verifyMailOfSender(string senderMail)
        {
            while (!InputDataValidator.valdiateMail(senderMail))
            {
                InputDataValidator.wrongSenderMailCounter++;
                if (InputDataValidator.wrongSenderMailCounter >= 3)
                {
                    Console.WriteLine("Sorry, you entered invalid mail 3 times !");
                    Console.WriteLine("Check your input parameters and try again.");
                    Environment.Exit(1);
                }
                Console.WriteLine(String.Format("Wrong e-mail ! You have {0} tries left. Enter e-mail of sender:", 3 - InputDataValidator.wrongSenderMailCounter));
                senderMail = Console.ReadLine();
            }
        }

        private void verifyPassword(string password)
        {
            while (!InputDataValidator.validatePass(password))
            {
                InputDataValidator.wrongPasswordCounter++;
                if (InputDataValidator.wrongPasswordCounter >= 3)
                {
                    Console.WriteLine("Sorry, you entered invalid password 3 times");
                    Console.WriteLine("Check your password requrements and try again.");
                    Environment.Exit(1);
                }
                Console.WriteLine(String.Format("Wrong password ! You have {0} tries left. Enter new password:", 3 - InputDataValidator.wrongPasswordCounter));
                password = Console.ReadLine();
            }
        }

        private void verifyMailOfReceiver(string receiverMail)
        {
            while (!InputDataValidator.valdiateMail(receiverMail))
            {
                InputDataValidator.wrongReceiverMailCounter++;
                if (InputDataValidator.wrongReceiverMailCounter >= 3)
                {
                    Console.WriteLine("Sorry, you entered invalid mail 3 times !");
                    Console.WriteLine("Check your input parameters and try again.");
                    Environment.Exit(1);
                }
                Console.WriteLine(String.Format("Wrong e-mail ! You have {0} tries left. Enter new e-mail:", 3 - InputDataValidator.wrongReceiverMailCounter));
                receiverMail = Console.ReadLine();
            }
        }

        private void verifyFilePath(string filePath)
        {
            while (!InputDataValidator.validateFilePath(filePath))
            {
                InputDataValidator.wrongFilePathCounter++;
                if (InputDataValidator.wrongFilePathCounter >= 3)
                {
                    Console.WriteLine("You entered wrong filepath 3 times !");
                    Console.WriteLine("Check your file directory and try again.");
                    Environment.Exit(1);
                }
                Console.WriteLine(string.Format("Wrong filepath ! You have {0} tries left. Enter filepath again:", 3 - InputDataValidator.wrongFilePathCounter));
                filePath = Console.ReadLine();
            }
        }

        private string createAggregatedFileAndReturnItsDirectory(string filePath)
        {

            string[] rawFileContent = FileReaderWriter.readFromFile(filePath);

            var countryPersonPair = DictionaryFactory.createCountryPersonDictionary(rawFileContent);

            var sortedDict = DictionaryFactory.sortCountriesByAverageScore(countryPersonPair);

            string aggregatedData = DataAggregator.collectAndSortData(countryPersonPair, sortedDict);

            string aggregatedDataFilePath = FileReaderWriter.writeAggregatedDataToFile(aggregatedData);

            return aggregatedDataFilePath;

        }

        private void sendMail(string senderMail, string pass, string receiverMail, string filePath)
        {

            new MailKitSender().sendMail(senderMail, pass, receiverMail, filePath);
        }
    }
}
