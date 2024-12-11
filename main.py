javascript:(function() {
    fetch('https://codeforces.com/data/submitSource', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-User-Sha1': 'cfd14df4c6e703378a1235a5848b2b66ec84d260',
            'X-User': 'a3a9c7af93ff7132172851f8216ba1b280c07481e450cf3b9637090f69bf534a9bee6d3c4e3ed5de',
            'x-csrf-token': '18a5e92dd6b6e16759bd40b373b3d1e2',
            'x-requested-with': 'XMLHttpRequest',
            'Accept': 'application/json, text/javascript, */*; q=0.01'
        },
        body: new URLSearchParams({
            'submissionId': '292453149'
        })
    })
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.error('Error:', error));
})();