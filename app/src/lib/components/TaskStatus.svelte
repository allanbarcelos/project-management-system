<script lang="ts">
  import { toast } from 'svelte-french-toast';
  
  export let taskId: number;
  export let currentStatus: string;
  export let onStatusChange: (status: string) => Promise<void>;

  const statusOptions = [
    { value: 'ToDo', label: 'To Do' },
    { value: 'InProgress', label: 'In Progress' },
    { value: 'Done', label: 'Done' }
  ];

  async function handleStatusChange(event: Event) {
    const select = event.target as HTMLSelectElement;
    const newStatus = select.value;
    
    try {
      await onStatusChange(newStatus);
      toast.success('Task status updated successfully');
    } catch (error) {
      toast.error('Failed to update task status');
      console.error('Status update error:', error);
    }
  }
</script>

<div class="task-status">
  <select 
    class="form-select form-select-sm" 
    value={currentStatus}
    on:change={handleStatusChange}
  >
    {#each statusOptions as option}
      <option value={option.value}>{option.label}</option>
    {/each}
  </select>
</div>

<style>
  .task-status {
    display: inline-block;
    min-width: 120px;
  }
  
  .form-select {
    cursor: pointer;
  }
</style> 