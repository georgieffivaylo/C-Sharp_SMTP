using System.Text.RegularExpressions;

public class InputDataValidator
{

    private static string REGEX_MAIL_PATTERN = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

    private static string SPECIAL_LETTERS = "~`!@#$%^&*()+=_-{}[]\\|:;”’?/<>,.";

    public static int wrongSenderMailCounter = 0;
    public static int wrongReceiverMailCounter = 0;
    public static int wrongPasswordCounter = 0;
    public static int wrongFilePathCounter = 0;

    public static bool valdiateMail(string mail)
    {

        Regex regex = new Regex(REGEX_MAIL_PATTERN);
        Match match = regex.Match(mail);

        return match.Success;
    }

    public static bool validatePass(string pass)
    {
        if(pass.Length < 8){
            return false;
        }

        bool hasAtLeastOneUpperCaseLetter = false;
        bool hasAtLeastOneLowerCaseLetter = false;
        bool hasAtLeastOneDigit = false;
        bool hasAtLeastOneSpecialCharacter = false;

        char[] passToCharArray = pass.ToCharArray();

        for (int i = 0; i < passToCharArray.Length; i++)
        {
            char currentSymbol = passToCharArray[i];
            if (char.IsUpper(currentSymbol))
            {
                hasAtLeastOneUpperCaseLetter = true;
            }
            if (char.IsLower(currentSymbol))
            {
                hasAtLeastOneLowerCaseLetter = true;
            }
            if (char.IsDigit(currentSymbol))
            {
                hasAtLeastOneDigit = true;
            }
            if (SPECIAL_LETTERS.Contains(currentSymbol))
            {
                hasAtLeastOneSpecialCharacter = true;
            }
        }

        bool[] requiredConditions = new bool[4];
        requiredConditions[0] = hasAtLeastOneUpperCaseLetter;
        requiredConditions[1] = hasAtLeastOneLowerCaseLetter;
        requiredConditions[2] = hasAtLeastOneDigit;
        requiredConditions[3] = hasAtLeastOneSpecialCharacter;

        int counterOfTrueConditions = 0;

        for (int i = 0; i < requiredConditions.Length; i++)
        {
            if (requiredConditions[i] == true)
            {
                counterOfTrueConditions++;
            }
        }

        if (counterOfTrueConditions >= 3)
        {
            return true;
        }

        return false;

    }

    public static bool validateFilePath(string filePath){

        try{
            System.IO.File.ReadAllText(filePath);
            return true;
        }catch(System.IO.FileNotFoundException){
            return false;
        }

    }

}