<script lang="ts">
  import { onMount } from 'svelte';
  import { get } from 'svelte/store';
  import { authStore } from '$lib/stores/authStore';
  import TaskList from '$lib/components/TaskList.svelte';
  import { toast } from 'svelte-french-toast';

  let tasks: Array<{
    id: number;
    title: string;
    description: string;
    status: string;
    dueDate?: string;
  }> = [];

  async function loadTasks() {
    const { token } = get(authStore);
    
    try {
      const response = await fetch(`${import.meta.env.VITE_API_URL}/tasks`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });

      if (response.ok) {
        tasks = await response.json();
      } else {
        throw new Error('Failed to load tasks');
      }
    } catch (error) {
      toast.error('Failed to load tasks');
      console.error('Error loading tasks:', error);
    }
  }

  onMount(() => {
    loadTasks();
  });
</script>

<div class="container-fluid px-4">
  <h1 class="mt-4">Tasks</h1>
  <ol class="breadcrumb mb-4">
    <li class="breadcrumb-item active">Tasks</li>
  </ol>
  
  <TaskList {tasks} />
</div> 