<script setup lang="ts">
import lvButton from 'lightvue/button'

function loginPleaseSign() {
  let domain = 'public.pleasesign.com.au/oauth'
  let client_id = 'ea80517bd7aacb26f6ad7c0904e1c900'
  let code_challenge = 'xv38n6K5bfRftBcnJP5IFpIDzbrF0E2oTI1x2Ep575w'
  let redirect_uri = 'http://localhost:5173/callback/'
  let state = '79560a1e48e74acc0049f94030628b40154e32cf98c77b4d'
  
  let loginUrl = `https://${domain}/authorize?
    response_type=code&
    code_challenge=${code_challenge}&
    code_challenge_method=S256&
    client_id=${client_id}&
    redirect_uri=${redirect_uri}&
    state=${state}`
  
  const popup = window.open(loginUrl, "oauth", "width=800,height=800");
  window.addEventListener("storage", async () => {
    const code = localStorage.getItem("pleaseSignCode");
    if (code) {
      popup.close();
      localStorage.removeItem("pleaseSignCode");
    }
  });
}

function loginMicrosoft() { }
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
  </div>
</template>
