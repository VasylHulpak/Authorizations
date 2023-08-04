<script setup lang="ts">
import lvButton from 'lightvue/button'

function loginPleaseSign() { }
function loginMicrosoft() { }
function loginLinkedin() { }
function loginInstagram() { }
</script>

<template>
  <div class="grid gap-20px text-center justify-center p-100px relative">
    <lvButton
        label="Authorise with PleaseSign"
        type="button"
        class="bg-orange text-white border-1px border-white border-solid h-40px p-6px w-300px"
        @click="loginPleaseSign"
    />
    <lvButton
        :outline="true"
        label="Authorise with Gmail"
        type="button"
        class="bg-white text-blue border-1px border-blue border-solid h-40px p-6px w-300px"
        @click="loginMicrosoft"
    />

    <lvButton
        :outline="true"
        label="Authorise with Linkedin"
        type="button"
        class="bg-white text-blue border-1px border-blue border-solid h-40px p-6px w-300px"
        @click="loginLinkedin"
    />

    <lvButton
        :outline="true"
        label="Authorise with Instagram"
        type="button"
        class="bg-white text-blue border-1px border-blue border-solid h-40px p-6px w-300px"
        @click="loginInstagram"
    />
  </div>
</template>
