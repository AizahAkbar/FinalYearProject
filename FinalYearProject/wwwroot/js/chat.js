$(document).ready(function () {
    let isOpen = false;

    $('#chatToggle').click(function () {
        isOpen = !isOpen;
        $('#chatContainer').slideToggle();
        $('.minimize-button').text(isOpen ? '−' : '+');
    });

    loadChatHistory();

    $('#messageInput').keypress(function (e) {
        if (e.which == 13) {
            sendMessage();
        }
    });

    $('#sendButton').click(function () {
        sendMessage();
    });

    function loadChatHistory() {
        $.get('/Chat/GetChatHistory', function (messages) {
            messages.forEach(function (message) {
                if (message.role === 'user' || message.role === 'assistant') {
                    appendMessage(message);
                }
            });
            scrollToBottom();
        });
    }

    function sendMessage() {
        var messageInput = $('#messageInput');
        var message = messageInput.val().trim();

        if (message) {
            appendMessage({
                role: 'user',
                content: message
            });

            $.ajax({
                url: '/Chat/SendMessage',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(message),
                success: function (response) {
                    if (response.role === 'assistant') {
                        appendMessage(response);
                    }
                    scrollToBottom();
                },
                error: function () {
                    alert('Error sending message');
                }
            });

            messageInput.val('');
        }
    }

    function appendMessage(message) {
        var messageHtml = `
            <div class="message ${message.role}">
                <div class="message-content">${message.content}</div>
            </div>
        `;
        $('#chatMessages').append(messageHtml);
        scrollToBottom();
    }

    function scrollToBottom() {
        var chatMessages = $('#chatMessages');
        chatMessages.scrollTop(chatMessages[0].scrollHeight);
    }
});
