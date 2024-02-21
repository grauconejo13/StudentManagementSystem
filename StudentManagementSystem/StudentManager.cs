using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using StudentManagementSystem;
using System;
using System.Collections.Generic;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using System.Threading.Channels;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Linq;

namespace StudentManagementSystem
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string MongoDBId { get; set; }
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Program { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal TotalFees { get; set; }

        public Student(string mongoDBId, int studentId, string name, string address, string program, DateTime birthdate, decimal totalFees)
        {
            this.MongoDBId = mongoDBId;
            this.StudentId = studentId;
            this.Name = name;
            this.Address = address;
            this.Program = program;
            this.BirthDate = birthdate;
            this.TotalFees = totalFees;
        }
    }

    public class StudentManager
    {
        private readonly IMongoCollection<Student> _collection;

        public StudentManager()
        {
            _collection = InitializeDatabase();
        }

        private IMongoCollection<Student> InitializeDatabase()
        {
            try
            {
                var client = new MongoClient("mongodb+srv://vievie:<password>@grauconejo13.hpjmuez.mongodb.net/?retryWrites=true&w=majority");
                var database = client.GetDatabase("StudentManagementDB");
                return database.GetCollection<Student>("Students");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error connecting to the database.", ex);
                // Handle connection failure
                //Console.WriteLine("Error connecting to the database: " + ex.Message);
                // return null;
            }
        }

        public bool IsConnectionValid()
        {
            return _collection != null;
        }

        public async void InsertStudent(Student student)
        {
            // Generate a new ObjectId
            student.MongoDBId = ObjectId.GenerateNewId().ToString();
            await _collection.InsertOneAsync(student);
        }

        public async void UpdateStudent(Student student, ObservableCollection<Student> students)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.StudentId, student.StudentId);
            var update = Builders<Student>.Update
                .Set(s => s.Name, student.Name)
                .Set(s => s.Address, student.Address)
                .Set(s => s.Program, student.Program)
                .Set(s => s.BirthDate, student.BirthDate)
                .Set(s => s.TotalFees, student.TotalFees);

            await _collection.UpdateOneAsync(filter, update);

            // Find the student in the ObservableCollection and update its properties
            var updatedStudent = students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if (updatedStudent != null)
            {
                updatedStudent.Name = student.Name;
                updatedStudent.Address = student.Address;
                updatedStudent.Program = student.Program;
                updatedStudent.BirthDate = student.BirthDate;
            }
        }
        public async void DeleteStudent(string mongoDBId)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.MongoDBId, mongoDBId);
            await _collection.DeleteOneAsync(filter);
        }

        public List<Student> GetAllStudents()
        {
            if (_collection != null)
            {
                return _collection.Find(_ => true).ToList();
            }
            return new List<Student>();
        }
    }
}