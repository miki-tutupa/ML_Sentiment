const app = Vue.createApp({
    data() {
        return {
            messageToPredict: null,
            sentiment: null,
            noToxicScore: null,
            toxicScore: null,
            sentimentColor: null,
            predictedLabel: 0
        }
    },
    methods: {
        initialState() {
            this.sentiment = null
        },
        initialStateTotal() {
            this.sentiment = null,
            this.messageToPredict = null
        },
        predictMessage() {
            // Simple POST request with a JSON body using fetch
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ comment: this.messageToPredict })
            };
            fetch("https://sentimentator.azurewebsites.net/predict", requestOptions)
                .then(response => response.json())
                .then(data => (
                    this.sentiment = data.predictedResult,
                    this.noToxicScore = data.score[0],
                    this.toxicScore = data.score[1],
                    this.sentimentColor = data.predictedLabel ? 'red' : 'green'
                ));
        }
    }
});

app.mount("#hero");