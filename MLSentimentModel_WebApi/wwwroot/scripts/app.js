const app = Vue.createApp({
    data() {
        return {
            info: null
        }
    },
    methods: {
        async getData() {
            try {
                let response = await fetch("https://sentimentator.azurewebsites.net/predict");
                this.info = await response.json();;
            } catch (error) {
                console.log(error);
            }
        },
    },
    created() {
        this.getData();
    },
});

app.mount("#hero");