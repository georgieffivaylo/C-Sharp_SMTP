using System;

namespace DotNETProject
{
    class Person
    {
        private string firstName;
        private string lastName;
        private string town;
        private int score;

        public Person(string firstName, string lastName, string town, int score)
        {
            if (this.validateStr(firstName))
            {
                throw new ArgumentException("First name cannot be empty");
            }
            else
            {
                this.firstName = firstName;
            }

            if (this.validateStr(lastName))
            {
                throw new ArgumentException("Last name cannot be empty");
            }
            else
            {
                this.lastName = lastName;
            }

            if (this.validateStr(town))
            {
                throw new ArgumentException("Town cannot be empty");
            }
            else
            {
                this.town = town;
            }

            if (score < 0)
            {
                throw new ArgumentException("Score cannot be a negative number");
            }else {
                this.score = score;
            }

        }

        public string getFirstName(){
            return this.firstName;
        }

        public string getLastName(){
            return this.lastName;
        }

        public string getTown(){
            return this.town;
        }

        public int getScore(){
            return this.score;
        }

        private bool validateStr(string str)
        {
            return String.IsNullOrEmpty(str);
        }

    }
}