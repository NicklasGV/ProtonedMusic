export interface EmailModel {
  to: string;
  subject: string;
  body: string;
}

export function resetEmail() {
  return {
      to: '',
      subject: '',
      body: '',
  }
}

