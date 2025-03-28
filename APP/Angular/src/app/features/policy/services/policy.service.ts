import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Policy, PolicyInput } from '../models/policy.model';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  private readonly apiUrl = `${environment.apiUrl}/policy`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Policy[]> {
    return this.http.get<Policy[]>(this.apiUrl);
  }

  getById(id: number): Observable<Policy> {
    return this.http.get<Policy>(`${this.apiUrl}/${id}`);
  }

  getComplete(id: number): Observable<Policy> {
    return this.http.get<Policy>(`${this.apiUrl}/${id}/complete`);
  }

  create(policy: PolicyInput): Observable<Policy> {
    return this.http.post<Policy>(this.apiUrl, policy);
  }

  update(id: number, policy: PolicyInput): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, policy);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}