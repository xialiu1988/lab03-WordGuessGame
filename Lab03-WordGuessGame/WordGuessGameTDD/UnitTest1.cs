using System;
using Xunit;
using Lab03_WordGuessGame;
namespace WordGuessGameTDD
{
    public class UnitTest1
    {
        [Fact]
        public void CanretrieveAllWordsFromFIle()
        {
            string path= "../../../fortest.txt";

            Assert.True(Program.ViewFile(path));

        }


        [Fact]
        public void CanAddWordToFIle()
        {
            string path = "../../../fortest.txt";
            string word = "rainbow";
            Assert.True(Program.AddWord(path,word));

        }

        [Fact]
        public void CannotAddWordToFIleIFIthasNumber()
        {
            string path = "../../../fortest.txt";
            string word = "nhu567";
            Assert.False(Program.AddWord(path, word));

        }
        [Fact]
        public void CannotAddAbunchOFSpaces()
        {
            string path = "../../../fortest.txt";
            string word = "        ";
            Assert.False(Program.AddWord(path, word));

        }

        [Fact]
        public void CannotAddWordContainsSpecialCharToFIle()
        {
            string path = "../../../fortest.txt";
            string word = " #@?.";
            Assert.False(Program.AddWord(path, word));

        }

        [Fact]
        public void CanDeleteWordFromFIle()
        {
            string path = "../../../fortest.txt";
            string word = "cat";
            Assert.Equal(word,Program.DeleteWord(path, word));

        }

        [Fact]
        public void CanDetectAletterIfexistsIntheRandomWord()
        {
            string input = "c";
            string word = "cat";
            Assert.True(Program.DetectALetter(word, input));

        }
        [Fact]
        public void CanDetectIfNotExsits()
        {
            string input = "p";
            string word = "cat";
            Assert.False(Program.DetectALetter(word, input));

        }
        [Fact]
        public void CanDetectifinputisnull()
        {
            string input = "";
            string word = "cat";
            Assert.False(Program.DetectALetter(word, input));

        }
    }
}
