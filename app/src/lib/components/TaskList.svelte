<script lang="ts">
  import { onMount } from 'svelte';
  import { get } from 'svelte/store';
  import { authStore } from '../stores/authStore';
  import TaskStatus from './TaskStatus.svelte';
  import { updateTaskStatus } from '../services/task';

  export let tasks: Array<{
    id: number;
    title: string;
    description: string;
    status: string;
    dueDate?: string;
  }> = [];

  async function handleStatusChange(taskId: number, newStatus: string) {
    await updateTaskStatus(taskId, newStatus);
    // Update the local task status
    const task = tasks.find(t => t.id === taskId);
    if (task) {
      task.status = newStatus;
      tasks = [...tasks]; // Trigger reactivity
    }
  }
</script>

<div class="card mb-4">
  <div class="card-header">
    <i class="fas fa-table me-1"></i>
    Tasks
  </div>
  <div class="card-body">
    <table class="table table-bordered table-hover">
      <thead>
        <tr>
          <th>Title</th>
          <th>Description</th>
          <th>Status</th>
          <th>Due Date</th>
        </tr>
      </thead>
      <tbody>
        {#each tasks as task}
          <tr>
            <td>{task.title}</td>
            <td>{task.description}</td>
            <td>
              <TaskStatus
                taskId={task.id}
                currentStatus={task.status}
                onStatusChange={(status) => handleStatusChange(task.id, status)}
              />
            </td>
            <td>{task.dueDate || 'No due date'}</td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
</div>

<style>
  .table {
    width: 100%;
  }
  
  th {
    background-color: #f8f9fa;
  }
</style> 