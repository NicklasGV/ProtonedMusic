export interface EmailModel {
  to: string;
  subject: string;
  body: string;
}

export function resetEmail() {
  return {
      id: 0,
      to: '',
      subject: '',
      body: '',
  }
}

