import { get } from 'svelte/store';
import { authStore } from '../stores/authStore';

const API_URL = import.meta.env.VITE_API_URL;

export async function updateTaskStatus(taskId: number, status: string): Promise<void> {
  const { token } = get(authStore);
  
  const response = await fetch(`${API_URL}/tasks/${taskId}/status`, {
    method: 'PATCH',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify({ status })
  });

  if (!response.ok) {
    throw new Error('Failed to update task status');
  }
} 