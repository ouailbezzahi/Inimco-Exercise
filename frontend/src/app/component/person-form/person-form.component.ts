import { Component } from '@angular/core';
import { IPerson, IPersonAnalysis } from '../../../../types';
import { PersonService } from '../../service/person.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-person-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './person-form.component.html',
  styleUrl: './person-form.component.css'
})
export class PersonFormComponent {
person: IPerson = {
    firstName: '',
    lastName: '',
    socialSkills: [''],
    socialAccounts: []
  };

  analysis?: IPersonAnalysis;
  errorMessage: string = '';

  constructor(private personService: PersonService) {}

  addSocialSkill() {
    this.person.socialSkills.push('');
  }

  removeSocialSkill(index: number) {
    this.person.socialSkills.splice(index, 1);
  }

  addSocialAccount() {
    this.person.socialAccounts.push({ type: '', address: '' });
  }

  removeSocialAccount(index: number) {
    this.person.socialAccounts.splice(index, 1);
  }

  onSubmit() {
    if (!this.person.firstName || !this.person.lastName) {
      this.errorMessage = 'First name and last name are required.';
      return;
    }

    // Filter out empty social skills
    this.person.socialSkills = this.person.socialSkills.filter(skill => skill.trim() !== '');

    this.personService.analyzePerson(this.person).subscribe({
      next: (analysis) => {
        this.analysis = analysis;
        this.errorMessage = '';
      },
      error: (error) => {
        this.errorMessage = 'An error occurred while analyzing the person.';
        console.error(error);
      }
    });
  }
}
