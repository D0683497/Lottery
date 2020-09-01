import { StudentAdd } from './../../models/student/student-add.model';
import { Student } from './../../models/student/student.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  urlRoot = 'http://localhost:5001/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getStudentsForRound(roundId: string): Observable<Student[]> {
    const url = `${this.urlRoot}/rounds/${roundId}/students`;
    return this.http.get<Student[]>(url, this.httpOptions);
  }

  getStudentForRound(roundId: string, studentId: string): Observable<Student> {
    const url = `${this.urlRoot}/rounds/${roundId}/students/${studentId}`;
    return this.http.get<Student>(url, this.httpOptions);
  }

  createStudent(roundId: string, student: StudentAdd): Observable<Student> {
    const url = `${this.urlRoot}/rounds/${roundId}/students`;
    return this.http.post(url, student, this.httpOptions).pipe(
      map((newStudent: Student) => newStudent)
    );
  }

}
