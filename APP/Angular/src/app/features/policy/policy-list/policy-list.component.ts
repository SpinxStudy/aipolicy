import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PolicyService } from '../services/policy.service';
import { Policy } from '../models/policy.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-policy-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './policy-list.component.html',
  styleUrls: ['./policy-list.component.css']
})
export class PolicyListComponent implements OnInit {
  policies: Policy[] = [];

  constructor(private policyService: PolicyService) {}

  ngOnInit(): void {
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.policyService.getAll().subscribe({
      next: (policies) => (this.policies = policies),
      error: (err) => console.error('Erro ao carregar políticas:', err)
    });
  }

  deletePolicy(id: number): void {
    if (confirm('Are you sure that you want to delete this policy?')) {
      this.policyService.delete(id).subscribe({
        next: () => this.loadPolicies(),
        error: (err) => console.error('Error to delete this policy:', err)
      });
    }
  }
}
