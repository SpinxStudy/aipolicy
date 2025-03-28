import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PolicyService } from '../services/policy.service';
import { Policy, PolicyInput } from '../models/policy.model';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-policy-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './policy-form.component.html',
  styleUrls: ['./policy-form.component.css']
})
export class PolicyFormComponent implements OnInit {
  policy: PolicyInput = { name: '' };
  isEditMode = false;
  policyId?: number;

  constructor(
    private policyService: PolicyService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.policyId = this.route.snapshot.params['id'];
    if (this.policyId) {
      this.isEditMode = true;
      this.loadPolicy(this.policyId);
    }
  }

  loadPolicy(id: number): void {
    this.policyService.getById(id).subscribe({
      next: (policy) => (this.policy = { name: policy.name, version: policy.version }),
      error: (err) => console.error('Loading error policies:', err)
    });
  }

  savePolicy(): void {
    if (this.isEditMode && this.policyId) {
      this.policyService.update(this.policyId, this.policy).subscribe({
        next: () => this.router.navigate(['/policies']),
        error: (err) => console.error('Updating error policy:', err)
      });
    } else {
      this.policyService.create(this.policy).subscribe({
        next: () => this.router.navigate(['/policies']),
        error: (err) => console.error('Creating error policy:', err)
      });
    }
  }
}
