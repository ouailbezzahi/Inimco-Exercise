export interface IPerson {
  firstName: string;
  lastName: string;
  socialSkills: string[];
  socialAccounts: ISocialAccount[];
}

export interface ISocialAccount {
  type: string;
  address: string;
}

export interface IPersonAnalysis {
  vowelsCount: number;
  consonantsCount: number;
  fullName: string;
  reversedFullName: string;
  person: IPerson;
}