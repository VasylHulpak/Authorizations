<script setup lang="ts">
import lvButton from 'lightvue/button'
import axios   from 'axios'

async function loginPleaseSign() {
  await axios
  .get('https://https://localhost:5001/api/auth/LogInWith', {
    params: {
      schema: 'pleaseSign'
    }
  });
}

async function loginGitHub() {
  await axios
  .get('https://https://localhost:5001/api/auth/LogInWith', {
    params: {
      schema: 'github'
    }
  });
}

async function loginMicrosoft() {
  await axios
  .get('https://https://localhost:5001/api/auth/LogInWith', {
    params: {
      schema: 'microsoft'
    }
  });
}

async function loginLinkedin() {
  await axios
  .get('https://https://localhost:5001/api/auth/LogInWith', {
    params: {
      schema: 'linkedIn'
    }
  });
}

async function loginInstagram() {
  await axios
  .get('https://https://localhost:5001/api/auth/LogInWith', {
    params: {
      schema: 'instagram'
    }
  });
}
</script>

<template>
  <div class="grid gap-20px text-center justify-center p-100px relative">
    <lvButton
        label="Authorise with PleaseSign"
        type="button"
        class="bg-orange text-white border-1px border-white border-solid h-40px p-6px w-240px"
        @click="loginPleaseSign"
        icon="light-icon-brand-pleasesign"
    />
    <lvButton
        :outline="true"
        label="Authorise with Gmail"
        type="button"
        class="bg-white text-blue border-1px border-blue border-solid h-40px p-6px w-240px"
        @click="loginMicrosoft"
        icon="light-icon-mail"
    />

    <lvButton
        :outline="true"
        label="Authorise with Linkedin"
        type="button"
        class="bg-blue text-white border-1px border-white border-solid h-40px p-6px w-240px"
        @click="loginLinkedin"
        icon="light-icon-brand-linkedin"
        
    />

    <lvButton
        :outline="true"
        label="Authorise with Instagram"
        type="button"
        class="instagram-btn border-1px border-blue border-solid h-40px p-6px w-240px"
        @click="loginInstagram"
        icon="light-icon-brand-instagram"
    />

    <lvButton
        :outline="true"
        label="Authorise with GitHub"
        type="button"
        class="bg-black text-white border-1px border-white border-solid h-40px p-6px w-240px"
        @click="loginGitHub"
        icon="light-icon-brand-github"
    />
  </div>
</template>

<style>
.instagram-btn {
  background: #f09433;
  background: -moz-linear-gradient(45deg, #f09433 0%, #e6683c 25%, #dc2743 50%, #cc2366 75%, #bc1888 100%);
  background: -webkit-linear-gradient(45deg, #f09433 0%, #e6683c 25%, #dc2743 50%, #cc2366 75%, #bc1888 100%);
  background: linear-gradient(45deg, #f09433 0%, #e6683c 25%, #dc2743 50%, #cc2366 75%, #bc1888 100%);
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f09433', endColorstr='#bc1888', GradientType=1);
}

.lv-button:enabled:hover {
  background: rgba(0, 0, 0, 0.9);
}
</style>
