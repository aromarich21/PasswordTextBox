using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PasswordUtils
    {
        public static int ComputePasswordScore(string pwd)
        {
            // Init Vars
            int score = 0;
            int iUpperCase = 0;
            int iLowerCase = 0;
            int iDigit = 0;
            int iSymbol = 0;
            int iRepeated = 1;
            Hashtable htRepeated = new Hashtable();
            int iMiddle = 0;
            int iMiddleEx = 1;
            int ConsecutiveMode = 0;
            int iConsecutiveUpper = 0;
            int iConsecutiveLower = 0;
            int iConsecutiveDigit = 0;
            string sAlphas = "abcdefghijklmnopqrstuvwxyz";
            string sNumerics = "01234567890";
            int nSeqAlpha = 0;
            int nSeqChar = 0;
            int nSeqNumber = 0;

            if (String.IsNullOrEmpty(pwd))
                return score;

            // Scan password
            foreach (char ch in pwd.ToCharArray())
            {
                // Count digits
                if (Char.IsDigit(ch))
                {
                    iDigit++;

                    if (ConsecutiveMode == 3)
                        iConsecutiveDigit++;
                    ConsecutiveMode = 3;
                }

                // Count uppercase characters
                if (Char.IsUpper(ch))
                {
                    iUpperCase++;
                    if (ConsecutiveMode == 1)
                        iConsecutiveUpper++;
                    ConsecutiveMode = 1;
                }

                // Count lowercase characters
                if (Char.IsLower(ch))
                {
                    iLowerCase++;
                    if (ConsecutiveMode == 2)
                        iConsecutiveLower++;
                    ConsecutiveMode = 2;
                }

                // Count symbols
                if (Char.IsSymbol(ch) || Char.IsPunctuation(ch))
                {
                    iSymbol++;
                    ConsecutiveMode = 0;
                }

                // Count repeated letters 
                if (Char.IsLetter(ch))
                {
                    if (htRepeated.Contains(Char.ToLower(ch))) iRepeated++;
                    else htRepeated.Add(Char.ToLower(ch), 0);

                    if (iMiddleEx > 1)
                        iMiddle = iMiddleEx - 1;
                }

                if (iUpperCase > 0 || iLowerCase > 0)
                {
                    if (Char.IsDigit(ch) || Char.IsSymbol(ch))
                        iMiddleEx++;
                }
            }

            // Check for sequential alpha string patterns (forward and reverse) 
            for (int s = 0; s < 23; s++)
            {
                string sFwd = sAlphas.Substring(s, 3);
                string sRev = Reverse(sFwd);
                if (pwd.ToLower().IndexOf(sFwd) != -1 || pwd.ToLower().IndexOf(sRev) != -1)
                {
                    nSeqAlpha++;
                    nSeqChar++;
                }
            }

            // Check for sequential numeric string patterns (forward and reverse)
            for (int s = 0; s < 8; s++)
            {
                string sFwd = sNumerics.Substring(s, 3);
                string sRev = Reverse(sFwd);
                if (pwd.ToLower().IndexOf(sFwd) != -1 || pwd.ToLower().IndexOf(sRev) != -1)
                {
                    nSeqNumber++;
                    nSeqChar++;
                }
            }

            // Score += 4 * Password Length
            score = 4 * pwd.Length;

            // if we have uppercase letetrs Score +=(number of uppercase letters *2)
            if (iUpperCase > 0)
            {
                score += ((pwd.Length - iUpperCase) * 2);
            }

            // if we have lowercase letetrs Score +=(number of lowercase letters *2)
            if (iLowerCase > 0)
            {
                score += ((pwd.Length - iLowerCase) * 2);
            }


            // Score += (Number of digits *4)
            score += (iDigit * 4);

            // Score += (Number of Symbols * 6)
            score += (iSymbol * 6);

            // Score += (Number of digits or symbols in middle of password *2)
            score += (iMiddle * 2);

            //requirments
            int requirments = 0;
            if (pwd.Length >= 8) requirments++;     // Min password length
            if (iUpperCase > 0) requirments++;      // Uppercase letters
            if (iLowerCase > 0) requirments++;      // Lowercase letters
            if (iDigit > 0) requirments++;          // Digits
            if (iSymbol > 0) requirments++;         // Symbols

            // If we have more than 3 requirments then
            if (requirments > 3)
            {
                // Score += (requirments *2) 
                score += (requirments * 2);
            }

            //
            // Deductions
            //
            // If only letters then score -=  password length
            if (iDigit == 0 && iSymbol == 0)
            {
                score -= pwd.Length;
            }

            // If only digits then score -=  password length
            if (iDigit == pwd.Length)
            {
                score -= pwd.Length;
            }

            // If repeated letters used then score -= (iRepeated * (iRepeated - 1));
            if (iRepeated > 1)
            {
                score -= (iRepeated * (iRepeated - 1));
            }

            // If Consecutive uppercase letters then score -= (iConsecutiveUpper * 2);
            score -= (iConsecutiveUpper * 2);

            // If Consecutive lowercase letters then score -= (iConsecutiveUpper * 2);
            score -= (iConsecutiveLower * 2);

            // If Consecutive digits used then score -= (iConsecutiveDigits* 2);
            score -= (iConsecutiveDigit * 2);

            // If password contains sequence of letters then score -= (nSeqAlpha * 3)
            score -= (nSeqAlpha * 3);

            // If password contains sequence of digits then score -= (nSeqNumber * 3)
            score -= (nSeqNumber * 3);
                        
            if (score < 1) score = 1;
            if (score > 100) score = 100;

            return score;
        }

        public static string GetPasswordStrengthText(int score)
        {
            if (score < 2)
            {
                return String.Format("Too short ({0})", score);
            }
            else if (score < 60)
            {
                return String.Format("Weak ({0})", score);
            }
            else if (score < 80)
            {
                return String.Format("Good ({0})", score);
            }
            else
            {
                return String.Format("Strong ({0})", score);
            }
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
